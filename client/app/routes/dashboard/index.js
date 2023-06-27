import { expr } from 'cx/ui';
import { Charts } from './Charts';
import Controller from './Controller';
import { KPI } from './KPI';
import { TopExpenses } from './TopExpenses';
import { TopProducts } from './TopProducts';
import { Repeater, computable } from 'cx/src/ui';

export default () => (
   <cx>
      <div class="bg-gray-50 overflow-auto" controller={Controller}>
         <div class="grid grid-cols-4 p-8 gap-8" style="grid-template-rows: auto; width: 1150px">
            <Repeater records-bind="$page.metrics" recordAlias="$metric">
               <KPI
                  title-bind="$metric.name"
                  value-bind="$metric.value"
                  unit-bind="$metric.unit"
                  icon="cash"
                  iconClass="bg-green-100 text-green-600"
                  change={0.102}
                  className={{
                     'opacity-10': expr('!{$page.charts.sales}'),
                  }}
               />
            </Repeater>
            <Charts />

            <div class="bg-white border col-span-2 p-6 rounded text-gray-600">
               <div class="mb-2">Top Products</div>
               <TopProducts />
            </div>
            <div class="bg-white border col-span-2 p-6 rounded text-gray-600">
               <div class="mb-2">Expense Breakout</div>
               <TopExpenses />
            </div>
         </div>
      </div>
   </cx>
);
