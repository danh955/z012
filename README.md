# z010 - Monthly stock change grid 

Create a grid of monthly stock changes.  Then sort by the ones that has the best long term trends. 
MSFT is a good example.  Plus pick a day in the past and see how it does in the future.

Left would be the stock symbols.  Top is each month.
Each cell will have the percentage change for each month.

#### Sorting algorithm

Count| Jan | Feb | Mar | Apr | May |
---|----|----|----|----|----|
3 | Up | Dw | Up | Up | Dw |
3 | Up | Up | Dn | Up | Dn |
2 | Dw | Dw | Up | Dw | Up |

Hmm, add more weight to the more recent.  Something to play with.
