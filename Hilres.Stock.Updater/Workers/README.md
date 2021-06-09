# TPL Dataflow

This process retrieves stock data and update the database.

## Action Blocks

### #1 Download and update the symbol list

1. Download list of stock Symbols.
2. Update symbols in database.
3. Add new symbols in database.
4. Send symbol to #2 Add stock prices.
5. Goto #4 Get stock frequency to update.

### #2 Add stock price to a symbol

1. Add stock prices to current date.
2. If there is a split or no stock data then
    - Send to #3 Download all stock prices.
3. else Send to #5 Update each StockFrequency.
 
### #3 Download all stock prices for a symbol

1. Download all the stock prices for a symbol.
2. Update database.
3. Send to #5 Update each StockFrequency.
3. Done.

### #4 Get stock frequency to update

1. Get all stock symbols that needs frequency update.
2. Exclude symbols in #2 and #3 queue.
3. Send to #5 Update each StockFrequency.

### #5 Update each stock frequency data

1. Update weekly, monthly, ... stock prices.

There will need to be a flag of some sort that indicates that
the stock frequency data needs to be updated.