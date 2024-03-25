import ChooseComponent from './ChooseComponent/ChooseComponent.tsx';
import videoCardIcon from 'assets/videocard-white-icon.png';
import processorIcon from 'assets/cpu-white-icon.png';
import styles from './ChooseComponents.module.css';
import componentTypes from 'enums/componentTypes.ts';
import React, { useEffect, useRef } from 'react';
import TProduct from 'types/TProduct.ts';
import TVideoCard from 'types/components/TVideoCard.ts';
import useComponents from 'hooks/useComponents.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import TComputerCase from 'types/components/TComputerCase.ts';
import TProcessor from 'types/components/TProcessor.ts';
import { TUseBuildComponents } from 'hooks/useBuild.ts';

function convertToTProduct<
  T extends TVideoCard<string> | TComputerCase<string> | TProcessor<string>,
>(components: T[]): TProduct[] {
  return components.map(component => {
    return {
      id: component.id,
      img: component.image,
      name: `${component.brand} ${component.model}`,
      description: component.description,
      price: component.price,
      power: component.power,
    };
  });
}

type ChooseComponentsProps = {
  components: TUseBuildComponents;
  setComponent: (
    newProduct: TProduct | null,
    componentType: keyof typeof componentTypes,
  ) => void;
};

const ChooseComponents: React.FC<ChooseComponentsProps> = props => {
  const { components: videoCards, getComponents: getVideoCards } =
    useComponents<TVideoCard<string>>('/VideoCard/getAllVideoCards');
  const { components: processors, getComponents: getProcessors } =
    useComponents<TProcessor<string>>('/Processor/getAllProcessors');
  const dispatch = useDispatch();
  const isLoaded = useRef(false);

  useEffect(() => {
    async function getComponents() {
      dispatch(setIsLoading(true));
      await getVideoCards();
      await getProcessors();
      dispatch(setIsLoading(false));
    }
    if (isLoaded.current) return;
    void getComponents();
    isLoaded.current = true;
  }, []);
  return (
    <section className={styles.blocks}>
      <ChooseComponent
        img={videoCardIcon}
        type={'videoCard'}
        title={'Видеокарта'}
        isImportant={true}
        errorType={props.components.videoCard.currentErrorType}
        searchTitle={'Выберите видеокарту'}
        products={convertToTProduct(videoCards)}
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getVideoCards().then(() => dispatch(setIsLoading(false)));
        }}
        currentProduct={props.components.videoCard.currentProduct}
        setCurrentProduct={props.setComponent}
      />
      <ChooseComponent
        img={processorIcon}
        type={'processor'}
        title={'Процессор'}
        isImportant={true}
        errorType={props.components.processor.currentErrorType}
        searchTitle={'Выберите процессор'}
        products={convertToTProduct(processors)}
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getProcessors().then(() => dispatch(setIsLoading(false)));
        }}
        currentProduct={props.components.processor.currentProduct}
        setCurrentProduct={props.setComponent}
      />
    </section>
  );
};

export default ChooseComponents;
