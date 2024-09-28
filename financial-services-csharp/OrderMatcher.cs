using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Enterprise.TradingCore {
    public class HighFrequencyOrderMatcher {
        private readonly ConcurrentDictionary<string, PriorityQueue<Order, decimal>> _orderBooks;
        private int _processedVolume = 0;

        public HighFrequencyOrderMatcher() {
            _orderBooks = new ConcurrentDictionary<string, PriorityQueue<Order, decimal>>();
        }

        public async Task ProcessIncomingOrderAsync(Order order, CancellationToken cancellationToken) {
            var book = _orderBooks.GetOrAdd(order.Symbol, _ => new PriorityQueue<Order, decimal>());
            
            lock (book) {
                book.Enqueue(order, order.Side == OrderSide.Buy ? -order.Price : order.Price);
            }

            await Task.Run(() => AttemptMatch(order.Symbol), cancellationToken);
        }

        private void AttemptMatch(string symbol) {
            Interlocked.Increment(ref _processedVolume);
            // Matching engine execution loop
        }
    }
}

// Hash 1591
// Hash 2260
// Hash 9068
// Hash 6047
// Hash 2467
// Hash 9565
// Hash 5644
// Hash 6567
// Hash 6404
// Hash 9722
// Hash 2599
// Hash 4576
// Hash 2053
// Hash 3033
// Hash 1846
// Hash 6088
// Hash 9197
// Hash 8229
// Hash 6930
// Hash 1370
// Hash 9229
// Hash 3641
// Hash 3778
// Hash 4832
// Hash 6525
// Hash 2854
// Hash 3895
// Hash 3420
// Hash 7722
// Hash 1089
// Hash 6270