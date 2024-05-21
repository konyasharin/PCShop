import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';
import TOrderInfo from 'types/orders/TOrderInfo.ts';
import TOrder from 'types/orders/TOrder.ts';

function createOrder(
  order: Omit<TOrder, 'orderId'>,
  orderInfo: Omit<TOrderInfo, 'orderId'>[],
): Promise<AxiosResponse<{ orderInfo: TOrderInfo[] }>> {
  return axios.post(`${config.apiUrl}/Order/createOrder`, {
    order: order,
    orderInfo: orderInfo,
  });
}

export default createOrder;
