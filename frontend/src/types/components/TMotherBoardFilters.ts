import TComponentFilters from 'types/components/TComponentFilters.ts';

type TMotherBoardFilters = TComponentFilters & {
  frequency: string;
  socket: string;
  chipset: string;
};

export default TMotherBoardFilters;
