using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VetMapp.Models;

namespace VetMapp.Helpers
{
    public sealed class VetHelper
    {

        public static string setColor(VetModel vet)
        {
            string color = null;

            if (vet.IsActive == true)
            {
                if (vet.IsMember == false)
                {
                    color = "gray";
                }

                else if (vet.IsMember == true)
                {
                    if (vet.IsConfirmed == false)
                    {
                        color = "purple";
                    }

                    else if (vet.IsConfirmed == true)
                    {

                        IList<object> list = vet.WorkingDaysAndHours.ToList();
                        Dictionary<string, string> dictionary = getDictionary(list[0]);

                        if (dictionary["allDay"] == "True")
                        {
                            color = "green";
                        }

                        else
                        {
                            var dayOfWeek = DateTime.Now.DayOfWeek.ToString();

                            #region Saturday
                            if (dayOfWeek == "Saturday")
                            {
                                if (dictionary.ContainsKey("saturday") && dictionary["saturday"] == "True")
                                {
                                    if (dictionary["saturdayOpenHour"] != null || dictionary["saturdayCloseHour"] != null)
                                    {
                                        var nowTime = DateTime.Now.ToLocalTime().TimeOfDay;
                                        var addTime = DateTime.Parse("1.01.2000 04:00:00").ToUniversalTime().TimeOfDay;
                                        var openTime = DateTime.Parse(dictionary["saturdayOpenHour"]).ToUniversalTime().TimeOfDay + addTime;
                                        var closeTime = DateTime.Parse(dictionary["saturdayCloseHour"]).ToUniversalTime().TimeOfDay + addTime;

                                        if ((openTime < nowTime) && (nowTime < closeTime))
                                        {
                                            color = "green";
                                        }

                                        else
                                        {
                                            if (dictionary["emergencyCall"] == "True") color = "orange";
                                            else color = "red";
                                        }
                                    }

                                    else
                                    {
                                        color = "gray";
                                    }
                                }

                                else
                                {
                                    color = "gray";
                                }
                            }
                            #endregion
                            #region Sunday
                            else if (dayOfWeek == "Sunday")
                            {
                                if (dictionary.ContainsKey("sunday") && dictionary["sunday"] == "True")
                                {
                                    if (dictionary["sundayOpenHour"] != null || dictionary["sundayCloseHour"] != null)
                                    {
                                        var nowTime = DateTime.Now.ToLocalTime().TimeOfDay;
                                        var addTime = DateTime.Parse("1.01.2000 04:00:00").ToUniversalTime().TimeOfDay;
                                        var openTime = DateTime.Parse(dictionary["sundayOpenHour"]).ToUniversalTime().TimeOfDay + addTime;
                                        var closeTime = DateTime.Parse(dictionary["sundayCloseHour"]).ToUniversalTime().TimeOfDay + addTime;

                                        if ((openTime < nowTime) && (nowTime < closeTime))
                                        {
                                            color = "green";
                                        }

                                        else
                                        {
                                            if (dictionary["emergencyCall"] == "True") color = "orange";
                                            else color = "red";
                                        }
                                    }

                                    else
                                    {
                                        color = "gray";
                                    }
                                }

                                else
                                {
                                    color = "gray";
                                }
                            }
                            #endregion
                            #region WeekDays
                            else
                            {
                                if (dictionary["workingOpenHour"] != null || dictionary["workingCloseHour"] != null)
                                {
                                    var nowTime = DateTime.Now.ToLocalTime().TimeOfDay;
                                    var addTime = DateTime.Parse("1.01.2000 04:00:00").ToUniversalTime().TimeOfDay;
                                    var openTime = DateTime.Parse(dictionary["workingOpenHour"]).ToUniversalTime().TimeOfDay + addTime;
                                    var closeTime = DateTime.Parse(dictionary["workingCloseHour"]).ToUniversalTime().TimeOfDay + addTime;

                                    if ((openTime < nowTime) && (nowTime < closeTime))
                                    {
                                        color = "green";
                                    }

                                    else
                                    {
                                        if (dictionary["emergencyCall"] != null)
                                        {
                                            if (dictionary["emergencyCall"] == "True")
                                            {
                                                color = "orange";
                                            }

                                            else
                                            {
                                                color = "red";
                                            }
                                        }

                                        else
                                        {
                                            color = "gray";
                                        }
                                    }
                                }

                                else
                                {
                                    color = "gray";
                                }
                            }
                            #endregion
                        }
                    }
                }

                else
                {
                    color = "gray";
                }
            }
            else
            {
                color = "gray";
            }

            return color;
        }

        public static Dictionary<string, string> getDictionary(object obj)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if (typeof(IDictionary).IsAssignableFrom(obj.GetType()))
            {
                IDictionary idict = (IDictionary)obj;

                foreach (object key in idict.Keys)
                {
                    if (idict[key] == null)
                    {
                        dictionary.Add(key.ToString(), null);
                    }

                    else
                    {
                        dictionary.Add(key.ToString(), idict[key].ToString());
                    }
                }
            }

            return dictionary;
        }

        public static StyleModel setStyle(VetModel vet)
        {
            if (vet.Color == "green")
            {
                IList<object> list = vet.WorkingDaysAndHours.ToList();
                Dictionary<string, string> dictionary = getDictionary(list[0]);

                if (dictionary["allDay"] != null && dictionary["allDay"] == "True")
                {
                    return new StyleModel()
                    {
                        StyleStatus = "7 / 24 Açık",
                        Foreground = "#2cb796",
                        Background = "#d2f2ed"
                    };
                }

                else
                {
                    return new StyleModel()
                    {
                        StyleStatus = "Açık",
                        Foreground = "#2cb796",
                        Background = "#d2f2ed"
                    };
                }
            }

            else if (vet.Color == "purple")
            {
                return new StyleModel()
                {
                    StyleStatus = "Belediye Merkezi",
                    Foreground = "#605bad",
                    Background = "#ddddf9"
                };
            }

            else if (vet.Color == "red")
            {
                return new StyleModel()
                {
                    StyleStatus = "Kapalı",
                    Foreground = "#dd3d3f",
                    Background = "#ffd2cc"
                };
            }

            else if (vet.Color == "orange")
            {
                return new StyleModel()
                {
                    StyleStatus = "Acil Aramalar",
                    Foreground = "#f5991a",
                    Background = "#ffeacb"
                };
            }

            else
            {
                return new StyleModel()
                {
                    StyleStatus = "Bilinmiyor",
                    Foreground = "#aaaaaa",
                    Background = "#ededed"
                };
            }
        }

        public static string setStatus(VetModel vet)
        {
            string status = null;

            if (vet.Color == "purple") status = "Barınak";
            else if (vet.Color == "gray") status = "Bilinmiyor";
            else if (vet.Color == "green") status = "Açık";
            else if (vet.Color == "red") status = "Kapalı";
            else if (vet.Color == "orange") status = "Yalnızca Acil Durumlar";

            return status;
        }
    }
}