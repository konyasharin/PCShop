import axios from 'api/axios.ts';
import config from '../../../config.ts';
import TCartProduct from 'types/TCartProduct.ts';
import { AxiosResponse } from 'axios';

function deleteFromCart(
  product: TCartProduct,
): Promise<AxiosResponse<{ id: number }>> {
  return axios.delete(
    `${config.apiUrl}/Cart/deleteProduct/${product.userId}/${product.productId}`,
  );
}

export default deleteFromCart;
