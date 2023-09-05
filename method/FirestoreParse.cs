using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Google.Cloud.Firestore;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.method
{
    public class FirestoreParse
    {
        // SessionCard
        private SessionCard ParseSessionCard(DocumentSnapshot item)
        {
            try
            {
                var reader = item.ToDictionary();
                var comment = new SessionCard()
                {
                    addCardTimestamp = Convert.ToInt32(reader[nameof(BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model.SessionCard.addCardTimestamp)]),
                    sessionId = reader[nameof(BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model.SessionCard.sessionId)].ToString(),

                };
                return comment;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<SessionCard>> ListSessionCard(Task<QuerySnapshot> task)
        {
            try
            {
                var ls = await task;

                List<SessionCard> list = new List<SessionCard>();


                foreach (var item in ls.Documents)
                {
                    var itemOut = ParseSessionCard(item);
                    list.Add(itemOut);
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<SessionCard> SessionCard(Task<DocumentSnapshot> task)
        {
            return ParseSessionCard(await task);
        }

        //Order List


        private SessionOrder ParseSessionOrder(DocumentSnapshot item)
        {
            try
            {
                var reader = item.ToDictionary();
                var comment = new SessionOrder()
                {

                    msp = reader[nameof(BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model.SessionOrder.msp)].ToString(),

                    
                    number = Convert.ToInt32(reader[nameof(BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model.SessionOrder.number)])
                };
                return comment;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<SessionOrder>> ListSessionOrder(Task<QuerySnapshot> task)
        {
            try
            {
                var ls = await task;

                List<SessionOrder> list = new List<SessionOrder>();


                foreach (var item in ls.Documents)
                {
                    var itemOut = ParseSessionOrder(item);
                    list.Add(itemOut);
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<SessionOrder> SessionOrder(Task<DocumentSnapshot> task)
        {
            return ParseSessionOrder(await task);
        }
    }
}
