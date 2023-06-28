import { Icon } from 'cx/widgets';
import { Text, computable } from 'cx/src/ui';

export const Metric = ({ title, value, unit, description }) => (
   <cx>
      <div class="bg-white w-fit border p-6 rounded transition-opacity duration-300">
         <div class="my-2 text-gray-600 flex flex-row gap-4 justify-evenly text-center align-middle">
            <div class="justify-center align-middle text-center">
               <Text value={title} />
            </div>
            <div
               tooltip={{
                  style: { color: 'black', backgroundColor: 'white' },
                  placement: 'up',
                  title: 'Description',
                  mouseTrap: true,
                  items: (
                     <cx>
                        <span text={description} />
                     </cx>
                  ),
               }}
            >
               <Icon
                  name="information-circle"
                  class="block p-2 rounded-full w-10 h-10"
                  className="text-green-600 bg-green-100"
               />
            </div>
         </div>
         <div class="text-emerald-950 font-bold leading-none" ws>
            <span text={value} /> <span class="text-sm" text={unit} />
         </div>
      </div>
   </cx>
);
