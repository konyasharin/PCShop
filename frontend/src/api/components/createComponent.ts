import { AxiosResponse } from 'axios';

function createComponent<T>(
  url: string,
  data: Omit<T, 'id'>,
): Promise<AxiosResponse<T>> {}

export default createComponent;
