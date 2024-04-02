import TComponentFilters from 'types/components/TComponentFilters.ts';

type TRAMFilters = TComponentFilters & {
  frequency: string;
  timings: string;
  ramDb: string;
};

export default TRAMFilters;
