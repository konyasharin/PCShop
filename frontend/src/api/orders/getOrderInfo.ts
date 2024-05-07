import axios from 'api/axios.ts';
import { AxiosResponse } from 'axios';
import config from '../../../config.ts';
import TOrderInfo from 'types/orders/TOrderInfo.ts';

function getOrderInfo(
  orderId: number,
): Promise<AxiosResponse<{ orderInfo: TOrderInfo[] }>> {
  return axios.get(`${config.apiUrl}/Order/getOrderInfo/${orderId}`);
}

export default getOrderInfo;
