using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace TedisNetDataSimulator.Workers
{
    public class EventWorker
    {
        private Thread _thread = null;
        private bool _isRun = false;
        private int _sqlCommandTimeout = 60;
        private int _lastTagId = 0;

        /// <summary>
        /// Frecuency create new tag value change and event
        /// </summary>
        private int _frecuency = 1000;

        public EventWorker() { }

        public void StartWorker(int frecuency)
        {
            _frecuency = frecuency;
            _isRun = true;
            _thread = new Thread(DoWork);
            _thread.Start();
        }

        public void StoptWorker()
        {
            _isRun = false;
            Thread.Sleep(1000);
            try
            {
                _thread.Abort();
            }
            catch
            {
                //
            }
            finally
            {
                _thread = null;
            }
        }

        private void DoWork()
        {
            while (_isRun)
            {
                try
                {
                    using (TedisNetEntities context = new TedisNetEntities())
                    {
                        context.Database.CommandTimeout = _sqlCommandTimeout;

                        // DESEMPARATS Estado
                        // A CT 146 Comando
                        // SYS.CONNECTION
                        // DI.009
                        // CDI.15003
                        // CDO.15009
                        // DO.003

                        // Get Tag
                        Tag tag = context.Tags.OrderBy(t => t.Id).FirstOrDefault(t => t.ShortName.Contains(" Estado") && t.Id > _lastTagId);
                        if (tag == null)
                        {
                            _lastTagId = 0;
                            tag = context.Tags.OrderBy(t => t.Id).FirstOrDefault(t => t.ShortName.Contains(" Estado") && t.Id > _lastTagId);
                        }

                        TagValue tagValue = context.TagValues.FirstOrDefault(t => t.TagId == tag.Id);
                        Random rnd = new Random();
                        if (tagValue == null)
                        {
                            tagValue = context.TagValues.Add(new TagValue()
                            {
                                TagId = tag.Id,
                                ValueInt = rnd.Next(1, 2),
                                SourceTimestamp = DateTime.Now,
                                UpdateTimestamp = DateTime.Now,
                                QualityId = 1,
                                QualitySourceId = 3
                            });
                            tagValue.ValueStr = tagValue.ValueInt == 1 ? "ABIERTO" : "CERRADO";
                            tagValue.ValueEnumId = tagValue.ValueInt == 1 ? 114 : 115;
                        }
                        else
                        {
                            tagValue.ValueInt = tagValue.ValueInt == 1 ? 2 : 1;
                            tagValue.ValueStr = tagValue.ValueInt == 1 ? "ABIERTO" : "CERRADO";
                            tagValue.ValueEnumId = tagValue.ValueInt == 1 ? 114 : 115;
                            tagValue.SourceTimestamp = DateTime.Now;
                            tagValue.UpdateTimestamp = DateTime.Now;
                            tagValue.QualityId = 1;
                            tagValue.QualitySourceId = 3;
                        }
                        context.SaveChanges();

                        TagValueChanx tagValueChange = new TagValueChanx
                        {
                            TagId = tag.Id,
                            ValueInt = tagValue.ValueInt,
                            QualityId = 1,
                            QualitySourceId = 3,
                            SourceTimestamp = tagValue.SourceTimestamp,
                            UpdateTimestamp = DateTime.Now,
                            ValueStr = tagValue.ValueStr,
                            ValueEnumId = tagValue.ValueEnumId
                        };
                        context.TagValueChanges.Add(tagValueChange);
                        context.SaveChanges();

                        context.Events.Add(new Event()
                        {
                            TagValueChangeId = tagValueChange.Id
                        });
                        context.SaveChanges();

                        _lastTagId = tag.Id;
                    }
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry("TedisNetDataSimulator", $"EventWorker DoWork. Message was:" + ExceptionUtility.GetMessageFromException(ex), EventLogEntryType.Warning);
                }
                Thread.Sleep(_frecuency);
            }
        }
    }
}
