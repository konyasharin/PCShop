import { AxiosResponse } from 'axios';
import axios from 'api/axios.ts';
import config from '../../../config.ts';
import TOneOfComponentsFilters from 'types/components/TOneOfComponentsFilters.ts';
import componentTypes from 'enums/componentTypes.ts';

export type TGetFilters = {
  [key in keyof TOneOfComponentsFilters]: string[];
};

function getFilters(
  componentType: keyof typeof componentTypes,
): Promise<AxiosResponse<TGetFilters>> {
  return axios.get(`${config.apiUrl}/${componentType}/getFilters`);
}

export default getFilters;
