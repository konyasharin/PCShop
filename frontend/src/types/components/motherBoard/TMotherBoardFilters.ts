import TComponentFilters from 'types/components/TComponentFilters.ts';

type TMotherBoardFilters = TComponentFilters & {
  socket: string;
  chipset: string;
};

export default TMotherBoardFilters;
