import TComponent from 'types/components/TComponent.ts';
import TComputerCaseFilters from 'types/components/computerCase/TComputerCaseFilters.ts';

type TComputerCase<T extends File | string> = TComponent<T> &
  TComputerCaseFilters;

export default TComputerCase;
