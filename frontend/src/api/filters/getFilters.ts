import { AxiosResponse } from 'axios';
import TVideoCardFilters from 'types/components/TVideoCardFilters.ts';
import TProcessorFilters from 'types/components/TProcessorFilters.ts';
import axios from 'api/axios.ts';
import config from '../../../config.ts';

export type TGetFilters<T extends TVideoCardFilters | TProcessorFilters> = {
  [key in keyof T]: string[];
};

function getFilters<T extends TVideoCardFilters | TProcessorFilters>(
  url: string,
): Promise<AxiosResponse<TGetFilters<T>>> {
  return axios.get(config.apiUrl + url);
}

export default getFilters;
