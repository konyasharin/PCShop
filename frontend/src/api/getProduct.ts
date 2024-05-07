import axios from 'api/axios.ts';
import { AxiosResponse } from 'axios';
import config from '../../config.ts';
import TProductInfo from 'types/TProductInfo.ts';

function getProduct(
  productId: number,
): Promise<AxiosResponse<{ product: TProductInfo }>> {
  return axios.get(`${config.apiUrl}/Products/getProduct/${productId}`);
}

export default getProduct;
