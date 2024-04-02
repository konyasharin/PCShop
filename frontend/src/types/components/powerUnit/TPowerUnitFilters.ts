import TComponentFilters from 'types/components/TComponentFilters.ts';

type TPowerUnitFilters = TComponentFilters & {
  battery: string;
  voltage: string;
};

export default TPowerUnitFilters;
