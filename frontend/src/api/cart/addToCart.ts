import TCartProduct from 'types/TCartProduct.ts';
import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';

function addToCart(
  newProduct: TCartProduct,
): Promise<AxiosResponse<{ data: TCartProduct }>> {
  return axios.post(`${config.apiUrl}/Cart/addProduct`, newProduct);
}

export default addToCart;
