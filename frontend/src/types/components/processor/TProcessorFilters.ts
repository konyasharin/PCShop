import TComponentFilters from 'types/components/TComponentFilters.ts';

type TProcessorFilters = TComponentFilters & {
  cores: string;
  clockFrequency: string;
  turboFrequency: string;
  heatDissipation: string;
};

export default TProcessorFilters;
