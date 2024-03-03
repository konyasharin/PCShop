import axios from '../../axios.ts';
import TComputerCase from 'types/components/TComputerCase.ts';
import TCreateComponentResponse from 'types/TCreateComponentResponse.ts';

export async function createComputerCase(
  data: Omit<TComputerCase<File>, 'id'>,
): Promise<TCreateComponentResponse<TComputerCase<string>>> {
  return await axios.post('ComputerCase/createComputerCase', data, {
    headers: { 'Content-Type': 'multipart/form-data' },
  });
}

export default createComputerCase;
