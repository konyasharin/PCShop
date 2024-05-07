import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';
import TOrder from 'types/orders/TOrder.ts';

function getOrder(orderId: number): Promise<AxiosResponse<{ order: TOrder }>> {
  return axios.get(`${config.apiUrl}/Order/getOrder/${orderId}`);
}

export default getOrder;
