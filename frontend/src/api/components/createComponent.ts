import { AxiosResponse } from 'axios';
import axios from 'api/axios.ts';

async function createComponent<T>(
  url: string,
  data: Omit<T, 'id'>,
): Promise<AxiosResponse<T>> {
  return await axios.post(url, data, {
    headers: { 'Content-Type': 'multipart/form-data' },
  });
}

export default createComponent;
