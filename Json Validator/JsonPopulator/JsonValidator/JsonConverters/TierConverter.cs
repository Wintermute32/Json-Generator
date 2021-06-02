using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using JsonValidator.JsonConverters;

namespace JsonValidator.JsonConverters
{
    class TierConverter
    {
        public List<Tier> GenerateTierList(List<FlowLayoutPanel> flowlist)
        {
            var popLists = flowlist.Find(x => x.Name == "tierPanel").Controls.OfType<GroupBox>().ToList();
            List<Tier> tierpoplist = new List<Tier>();

            foreach (var x in popLists)
            {
                Tier tier = new Tier();
                var combos = x.Controls.OfType<ComboBox>().ToList();
                var texts = x.Controls.OfType<TextBox>().ToList();

                int comboCount = combos.Count;

                switch (comboCount)
                {
                    case 3:
                        {
                            try
                            {
                                tier.cost = Convert.ToInt32(texts[0].Text);
                                tier.numPulls = Convert.ToInt32(combos[0].Text);
                                tier.guarantee = new Guarantee()
                                {
                                    SpecificPopId = combos[1].Text,
                                    specificPopAmount = combos[2].Text
                                };
                            }
                            catch
                            {
                                tier.cost = 0;
                                tier.numPulls = 0;
                                tier.guarantee = new Guarantee()
                                {
                                    SpecificPopId = "You didn't fill",
                                    specificPopAmount = "this out right",
                                };
                            }
                            break;
                        }

                    case 2:
                        {
                            try
                            {
                                tier.cost = Convert.ToInt32(texts[0].Text);
                                tier.numPulls = Convert.ToInt32(combos[0].Text);
                                tier.guarantee = new Guarantee() { LuckyPopPrize = Convert.ToBoolean(combos[1].Text) };
                            }

                            catch
                            {
                                tier.cost = 0;
                                tier.numPulls = 0;
                                tier.guarantee = new Guarantee() { LuckyPopPrize = false };
                            }
                            break;
                        }

                    case 1:
                        {
                            try
                            {
                                tier.cost = Convert.ToInt32(texts[0].Text);
                                tier.numPulls = Convert.ToInt32(combos[0].Text);
                            }
                            catch (Exception)
                            {
                                tier.numPulls = 0;
                                tier.cost = 0;
                            }
                            break;
                        }

                }
                tierpoplist.Add(tier);
            }
            return tierpoplist;
        }
    }
}
