using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Google.Cloud.Firestore;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.PARSE_DATA.FIRESTORE
{
    public static class PARSE_MAP
    {
        public static SessionCard ParseSessionCard(DocumentSnapshot item)
        {
            try
            {
                var reader = item.ToDictionary();
                var comment = new SessionCard()
                {
                    addCardTimestamp = Convert.ToInt32(reader[nameof(model.SessionCard.addCardTimestamp)]),
                    sessionId = reader[nameof(model.SessionCard.sessionId)].ToString(),

                };
                return comment;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static SessionOrder ParseSessionOrder(DocumentSnapshot item)
        {
            try
            {
                var reader = item.ToDictionary();
                var comment = new SessionOrder()
                {

                    msp = reader[nameof(model.SessionOrder.msp)].ToString(),


                    number = Convert.ToInt32(reader[nameof(model.SessionOrder.number)])
                };
                return comment;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
