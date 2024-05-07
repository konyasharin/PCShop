import { AxiosResponse } from 'axios';
import axios from 'api/axios.ts';
import config from '../../../config.ts';
import TOneOfComponentsFilters from 'types/components/TOneOfComponentsFilters.ts';
import componentTypes from 'enums/componentTypes.ts';

/**
 * Тип получаемых фильтров
 */
export type TGetFilters = {
  [key in keyof TOneOfComponentsFilters]: string[];
};

/**
 * Получает фильтры определенного типа компонентов
 * @param componentType тип компонента
 */
function getFilters(
  componentType: keyof typeof componentTypes,
): Promise<AxiosResponse<TGetFilters>> {
  return axios.get(`${config.apiUrl}/${componentType}/getFilters`);
}

export default getFilters;
