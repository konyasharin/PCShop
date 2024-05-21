import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';
import TCartProduct from 'types/TCartProduct.ts';

function getCart(
  userId: number,
): Promise<AxiosResponse<{ productsArray: TCartProduct[] }>> {
  return axios.get(`${config.apiUrl}/Cart/getProducts?userId=${userId}`);
}

export default getCart;
