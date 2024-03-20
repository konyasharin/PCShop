import TComponent from 'types/components/TComponent.ts';
import TComponentFilters from 'types/components/TComponentFilters.ts';
import TProcessorFilters from 'types/components/TProcessorFilters.ts';

type TProcessor<T extends string | File> = TComponent<T> &
  TComponentFilters &
  TProcessorFilters;

export default TProcessor;
