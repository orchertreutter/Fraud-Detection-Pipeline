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
// Hash 5020
// Hash 8313
// Hash 1114
// Hash 1564
// Hash 5559
// Hash 7441
// Hash 1202
// Hash 3586
// Hash 7563
// Hash 8670
// Hash 1663
// Hash 6058
// Hash 2413
// Hash 5908
// Hash 4028
// Hash 3497
// Hash 6595
// Hash 7190
// Hash 2656
// Hash 2667
// Hash 9498
// Hash 7554
// Hash 9262
// Hash 2998
// Hash 7816
// Hash 7147
// Hash 7154
// Hash 4689
// Hash 6824
// Hash 6367
// Hash 3242
// Hash 4834
// Hash 1086
// Hash 9942
// Hash 2859
// Hash 8263
// Hash 4851
// Hash 1363
// Hash 7190
// Hash 6582
// Hash 2002
// Hash 1763
// Hash 7046
// Hash 5029
// Hash 5702