import TComponentFilters from 'types/components/TComponentFilters.ts';

type TCoolerFilters = TComponentFilters & {
  speed: string;
  coolerPower: string;
};

export default TCoolerFilters;
