import { enableTooltips, enableMsgBoxAlerts, Toast, Tooltip } from 'cx/widgets';
import { registerToastImplementation } from '../util/toasts';

enableTooltips();
enableMsgBoxAlerts();

Tooltip.prototype.placementOrder =
   'up down right left up-right up-left right-up right-down down-right down-left left-up left-down';

registerToastImplementation({
   showErrorToast: (err, title) => {
      if (err.message) err = err.message;
      Toast.create({
         items: (
            <cx>
               <div>{err}</div>
            </cx>
         ),
         timeout: 2500,
         mod: 'error',
         placement: 'top-right',
      }).open();
   },
   showInfoToast: (content) => {
      Toast.create({
         items: (
            <cx>
               <div class="text-sm">{content}</div>
            </cx>
         ),
         timeout: 2500,
         mod: 'dark',
         placement: 'top-right',
      }).open();
   },
});

export default <cx></cx>;
