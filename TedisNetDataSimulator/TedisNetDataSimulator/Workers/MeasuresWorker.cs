using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace TedisNetDataSimulator.Workers
{
    public class MeasuresWorker
    {
        private Thread _thread = null;
        private bool _isRun = false;        
        private int _lastTagId = 0;
        private const int _tagValuesToUpdate = 1500;
        private const int _sqlCommandTimeout = 60;

        /// <summary>
        /// Frecuency change tag value
        /// </summary>
        private int _frecuency = 1000;

        public MeasuresWorker() { }

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

        private double NextRandomDouble(Random rnd)
        {
            // Parte entera
            double measureValue = rnd.Next(0, 1000);
            // Parte decimal
            measureValue += rnd.Next(0, 99) * 0.01;

            return measureValue;
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

                        // AI.30007
                        // AI.30000
                        // AI.30008

                        // Get Tag
                        List<Tag> tags = context.Tags.Where(t => t.ShortName.StartsWith("AI.") && t.Id > _lastTagId).OrderBy(t => t.Id).Take(_tagValuesToUpdate).ToList();
                        if (tags == null || tags.Count == 0)
                        {
                            _lastTagId = 0;
                            tags = context.Tags.OrderBy(t => t.Id).Where(t => t.ShortName.StartsWith("AI.") && t.Id > _lastTagId).Take(_tagValuesToUpdate).ToList();
                        }

                        List<int> tagIds = tags.Select(t => t.Id).ToList();
                        List<TagValue> tagValues = context.TagValues.Where(t => tagIds.Contains(t.TagId)).ToList();
                        Random rnd = new Random();
                        if (tagValues.Count != tags.Count)
                        {
                            foreach (var tag in tags)
                            {
                                var tagValue = tagValues.FirstOrDefault(tv => tv.TagId == tag.Id);
                                if (tagValue == null)
                                {
                                    tagValue = context.TagValues.Add(new TagValue()
                                    {
                                        TagId = tag.Id,
                                        ValueFloat = NextRandomDouble(rnd),
                                        SourceTimestamp = DateTime.Now,
                                        UpdateTimestamp = DateTime.Now,
                                        QualityId = 1,
                                        QualityDetailId = 1,
                                        QualitySourceId = 1
                                    });
                                    tagValue.ValueStr = tagValue.ValueFloat.Value.ToString("0,00");
                                }
                            }
                        }
                        else
                        {
                            DateTime sourceTimestamp = DateTime.Now;
                            DateTime updateTimestamp = DateTime.Now;
                            for (int i = 0; i < tagValues.Count; i++)
                            {
                                double newValueFloat = NextRandomDouble(rnd);
                                if (tagValues[i].ValueFloat == null)
                                {
                                    tagValues[i].ValueFloat = newValueFloat;
                                }
                                else
                                {
                                    // Force different value
                                    while (tagValues[i].ValueFloat.Value == newValueFloat)
                                    {
                                        newValueFloat = NextRandomDouble(rnd);
                                    }
                                    tagValues[i].ValueFloat = newValueFloat;
                                }
                                tagValues[i].ValueStr = tagValues[i].ValueFloat.Value.ToString("0.00");
                                tagValues[i].SourceTimestamp = sourceTimestamp;
                                tagValues[i].UpdateTimestamp = updateTimestamp;
                                tagValues[i].QualityId = 1;
                                tagValues[i].QualityDetailId = 1;
                                tagValues[i].QualitySourceId = 1;
                            }
                        }
                        context.SaveChanges();

                        _lastTagId = tags.Max(t => t.Id);
                    }
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry("TedisNetDataSimulator", $"MeasuresWorker DoWork. Message was:" + ExceptionUtility.GetMessageFromException(ex), EventLogEntryType.Warning);
                }
                Thread.Sleep(_frecuency);
            }
        }
    }
}
