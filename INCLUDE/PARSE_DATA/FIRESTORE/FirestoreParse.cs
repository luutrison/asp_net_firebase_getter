using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Google.Cloud.Firestore;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.PARSE_DATA.FIRESTORE
{
    public class FirestoreParse
    {
        // SessionCard
    

        public async Task<List<SessionCard>> ListSessionCard(Task<QuerySnapshot> task)
        {
            try
            {
                var ls = await task;

                List<SessionCard> list = new List<SessionCard>();


                foreach (var item in ls.Documents)
                {
                    var itemOut = PARSE_MAP.ParseSessionCard(item);
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
            return PARSE_MAP.ParseSessionCard(await task);
        }

        //Order List


        

        public async Task<List<SessionOrder>> ListSessionOrder(Task<QuerySnapshot> task)
        {
            try
            {
                var ls = await task;

                List<SessionOrder> list = new List<SessionOrder>();


                foreach (var item in ls.Documents)
                {
                    var itemOut = PARSE_MAP.ParseSessionOrder(item);
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
            return PARSE_MAP.ParseSessionOrder(await task);
        }
    }
}
