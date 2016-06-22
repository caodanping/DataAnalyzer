using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAnalyzer
{
    public partial class MainForm : Form
    {
        enum State
        {
            Running, Stopped, Canceling
        }

        private String inputFileName;
        private String saveFileName;
        private int progress = 0;
        private long readBytes = 0;
        private int importedRecords = 0;
        private long totalBytes = 0;
        private DateTime startTime;
        private bool importCompleted = false;
        private int writeCount = 0;
        private int totalCount = 0;

        private State state = State.Stopped;

        public MainForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (state == State.Stopped)
            {
                inputFileName = inFileTextBox.Text;
                if (String.IsNullOrEmpty(inputFileName))
                {
                    MessageBox.Show("必须要指定输入数据文件！");
                    return;
                }

                saveFileName = outFileTextBox.Text;
                if (String.IsNullOrEmpty(saveFileName))
                {
                    MessageBox.Show("必须要指定数据文件！");
                    return;
                }
                ResultState();
                startTime = DateTime.Now;
                state = State.Running;

                importDataWorker.RunWorkerAsync();
                timer.Start();

                startButton.Text = "停止统计";
                progressBar.Visible = true;
            }
            else if(state == State.Running)
            {
                importDataWorker.CancelAsync();
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "csv文件(逗号或Tab分隔)|*.csv|文本文件(逗号或Tab分隔)|*.txt";
            dialog.Multiselect = false;
            if (DialogResult.OK == dialog.ShowDialog())
            {
                inFileTextBox.Text = dialog.FileName;
            }
            else
            {
                inFileTextBox.Text = String.Empty;
            }
        }

        private void importDataWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (StreamReader reader = File.OpenText(inputFileName))
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SQLite");
                using (DbConnection conn = factory.CreateConnection())
                {
                    conn.ConnectionString = "Data Source=data.db";
                    conn.Open();
                    ClearData(conn);
                    DbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO data(text) VALUES(?)";
                    totalBytes = reader.BaseStream.Length;
                    DbTransaction tx = conn.BeginTransaction();
                    for (String line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    {
                        if (importDataWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                        readBytes += (line.Length + 1);
                        if (String.IsNullOrWhiteSpace(line)) continue;
                        String[] cols = line.Split(',', ' ', '\t');
                        foreach (String col in cols)
                        {
                            string val = col.Trim();

                            if (String.IsNullOrEmpty(val)) continue;

                            cmd.Parameters.Clear();
                            DbParameter param = cmd.CreateParameter();
                            param.DbType = DbType.String;
                            param.Value = val;
                            cmd.Parameters.Add(param);
                            cmd.ExecuteNonQuery();
                            importedRecords++;
                        }
                        int prog = (int)(readBytes * 100 / totalBytes);
                        if (prog != progress)
                        {
                            progress = prog;
                            importDataWorker.ReportProgress(prog);
                        }
                    }
                    tx.Commit();

                    progress = 0;
                    totalCount = importedRecords;
                    importCompleted = true;
                    importDataWorker.ReportProgress(progress);

                    startTime = DateTime.Now;

                    using (StreamWriter writer = new StreamWriter(File.Open(saveFileName, FileMode.OpenOrCreate)))
                    {
                        DbCommand query = conn.CreateCommand();
                        query.CommandText = "SELECT text, count(text) AS cnt FROM data GROUP BY text";
                        using (DbDataReader dbReader = query.ExecuteReader())
                        {
                            while (dbReader.Read()) 
                            {
                                if (importDataWorker.CancellationPending)
                                {
                                    e.Cancel = true;
                                    break;
                                }
                                writer.WriteLine(dbReader.GetString(0) + "," + dbReader.GetInt32(1));
                                writeCount++;

                                int prog = (int)(writeCount * 100 / totalCount);
                                if (prog != progress)
                                {
                                    progress = prog;
                                    importDataWorker.ReportProgress(prog);
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private static void ClearData(DbConnection conn)
        {
            DbTransaction tx = conn.BeginTransaction();
            DbCommand clearCmd = conn.CreateCommand();
            clearCmd.CommandText = "DELETE FROM data";
            clearCmd.ExecuteNonQuery();
            tx.Commit();
        }

        private void importDataWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Maximum = 100;
            progressBar.Value = progress;
        }

        private void importDataWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Stop();

            Ready();

            if (e.Error != null)
            {
                MessageBox.Show("处理发生错误！");
            }
            else if (e.Cancelled)
            {
                labelMessage.Text = "操作已取消.";
            }
            else
            {
                labelMessage.Text = "处理完成.";
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!importCompleted)
            {
                TimeSpan cost = DateTime.Now - startTime;
                long ticks = (long)((double)cost.Ticks * ((double)totalBytes / readBytes));
                TimeSpan total = new TimeSpan(ticks);
                TimeSpan left = total.Subtract(cost);
                labelMessage.Text = "已导入:" + importedRecords + ".   预估剩余时间:" + ToTimeSpanString(ref left);
            }
            else
            {
                TimeSpan cost = DateTime.Now - startTime;
                long ticks = (long)((double)cost.Ticks * ((double)totalBytes / readBytes));
                TimeSpan total = new TimeSpan(ticks);
                TimeSpan left = total.Subtract(cost);
                labelMessage.Text = "已写入:" + writeCount + "/" + totalCount + ".   预估剩余时间:" + ToTimeSpanString(ref left);
            }
        }

        private string ToTimeSpanString(ref TimeSpan timespan)
        {
            String txt = "";
            if (timespan.Hours > 0)
            {
                txt = timespan.Hours + "时:" + timespan.Minutes + "分";
            }
            else if (timespan.Minutes > 0)
            {
                txt = timespan.Minutes + "分:" + timespan.Seconds + "秒";
            }
            else
            {
                txt = timespan.Seconds + "秒";
            }
            return txt;
        }

        private void Ready()
        {
            state = State.Stopped;
            startButton.Text = "开始统计";
            progressBar.Visible = false;
        }

        private void ResultState()
        {
            progress = 0;
            progressBar.Value = 0;
            readBytes = 0;
            importedRecords = 0;
            totalBytes = 0;
            importCompleted = false;
            writeCount = 0;
            totalCount = 0;
            state = State.Stopped;
        }


        private void outFileSelectButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "csv文件(逗号分隔)|*.csv|文本文件(逗号分隔)|*.txt";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                outFileTextBox.Text = dlg.FileName;
            }
        }
    }
}
