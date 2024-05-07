import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';
import TOrder from 'types/orders/TOrder.ts';

function getOrders(
  limit: number,
  offset: number,
): Promise<AxiosResponse<{ orders: TOrder[] }>> {
  return axios.get(
    `${config.apiUrl}/Order/getOrders?limit=${limit}&offset=${offset}`,
  );
}

export default getOrders;
