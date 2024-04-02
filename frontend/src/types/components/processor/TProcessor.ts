import TComponent from 'types/components/TComponent.ts';
import TProcessorFilters from 'types/components/processor/TProcessorFilters.ts';

type TProcessor<T extends string | File> = TComponent<T> & TProcessorFilters;

export default TProcessor;
