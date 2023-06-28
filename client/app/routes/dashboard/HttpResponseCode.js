import { CategoryAxis, Chart, Gridlines, Legend, NumericAxis, ColumnGraph } from 'cx/charts';
import { Svg } from 'cx/svg';
import { KeySelection } from 'cx/ui';

export const HttpResponseCode = () => (
   <cx>
      <div class="bg-white border col-span-4 px-6 py-4 rounded">
         <div class="flex items-center">
            <div class="mr-auto text-gray-600">Http Response Code Distribution</div>
            <Legend />
         </div>
         <Svg class="w-full h-[350px] text-gray-500">
            <Chart
               margin="30 10 30 45"
               axes={{
                  x: {
                     type: CategoryAxis,
                     hideLine: true,
                     hideTicks: true,
                     snapToTicks: 0,
                     labelWrap: true,
                     labelOffset: 15,
                     labelRotation: -45,
                     labelDy: '0.3em',
                     labelAnchor: 'end',
                     labelLineCountDyFactor: -0.5,
                  },
                  y: {
                     type: NumericAxis,
                     vertical: true,
                     snapToTicks: 1,
                  },
               }}
            >
               <Gridlines />
               <ColumnGraph
                  data-bind="$page.chartMetrics"
                  colorIndex={0}
                  active-bind="$page.showCount"
                  name="Count"
                  size={0.3}
                  offset={-0.15}
                  xField="code"
                  yField="count"
                  selection={{
                     type: KeySelection,
                     bind: '$page.selected.code',
                     keyField: 'code',
                  }}
               />
            </Chart>
         </Svg>
      </div>
   </cx>
);
