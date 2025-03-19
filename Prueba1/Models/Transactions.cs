using Firebase.Auth;
using Google.Cloud.Firestore;
using Prueba1.Firebase;

namespace Prueba1.Models
{
    public class TransactionsModel
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
    }

    public class TransactionsHelper
    {
        public static async Task<List<TransactionsModel>> GetTransactions()
        {
            List<TransactionsModel> transactionsList = new List<TransactionsModel>();
            Query query = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("Transactions");
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            foreach (var item in querySnapshot)
            {
                Dictionary<string, object> data = item.ToDictionary();
                transactionsList.Add(new TransactionsModel
                {
                    FromUser = data["fromUser"].ToString(),
                    ToUser = data["toUser"].ToString(),
                    Amount = Convert.ToInt32(data["amount"]),
                    Date = data["date"].ToString(),
                });
            }
            return transactionsList;
        }

        public async Task<bool> RegisterTransaction (TransactionsModel transaction)
        {
            try
            {
                FirestoreDb fdb = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId);
                CollectionReference coll = fdb.Collection("Transactions");
                Dictionary<string, object> newTrasaction = new Dictionary<string, object>
                {
                    {"fromUser",transaction.FromUser },
                    {"toUser",transaction.ToUser },
                    {"amount",transaction.Amount },
                    {"date",transaction.Date },
                };

                await coll.AddAsync(newTrasaction);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

    }
}
