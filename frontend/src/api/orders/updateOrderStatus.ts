import OrderStatuses from 'enums/orderStatuses.ts';
import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';

function updateOrderStatus(
  orderId: number,
  newOrderStatus: keyof typeof OrderStatuses,
): Promise<
  AxiosResponse<{ orderId: number; newStatus: keyof typeof OrderStatuses }>
> {
  return axios.put(
    `${config.apiUrl}/Order/updateOrderStatus/${orderId}?newStatus=${newOrderStatus}`,
  );
}

export default updateOrderStatus;
