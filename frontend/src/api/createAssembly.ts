import axios from 'api/axios.ts';
import config from '../../config.ts';
import TAssembly from 'types/TAssembly.ts';
import { AxiosResponse } from 'axios';

async function createAssembly(
  data: Omit<TAssembly, 'id'>,
): Promise<AxiosResponse<TAssembly>> {
  return await axios.post(`${config.apiUrl}/assembly/create`, data);
}

export default createAssembly;
