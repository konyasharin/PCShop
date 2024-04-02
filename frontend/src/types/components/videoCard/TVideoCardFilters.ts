import TComponentFilters from 'types/components/TComponentFilters.ts';

type TVideoCardFilters = TComponentFilters & {
  memoryDb: string;
  memoryType: string;
};

export default TVideoCardFilters;
