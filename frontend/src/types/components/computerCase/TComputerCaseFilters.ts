import TComponentFilters from 'types/components/TComponentFilters.ts';

type TComputerCaseFilters = TComponentFilters & {
  material: string;
  height: string;
  depth: string;
  width: string;
};

export default TComputerCaseFilters;
