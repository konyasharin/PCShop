import axios from 'api/axios.ts';
import config from '../../../config.ts';
import componentTypes from 'enums/componentTypes.ts';
import { AxiosResponse } from 'axios';

function deleteComponent(
  componentType: keyof typeof componentTypes,
  productId: number,
): Promise<AxiosResponse<{ id: number }>> {
  return axios.delete(`${config.apiUrl}/${componentType}/delete/${productId}`);
}

export default deleteComponent;
