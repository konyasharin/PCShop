import { AxiosResponse } from 'axios';
import axios from 'api/axios.ts';
import componentTypes from 'enums/componentTypes.ts';
import config from '../../../config.ts';
import TOneOfComponents from 'types/components/TOneOfComponents.ts';

async function createComponent(
  componentType: keyof typeof componentTypes,
  data: Omit<TOneOfComponents<File>, 'productId'>,
): Promise<AxiosResponse<TOneOfComponents<string>>> {
  return await axios.post(`${config.apiUrl}/${componentType}/create`, data, {
    headers: { 'Content-Type': 'multipart/form-data' },
  });
}

export default createComponent;
