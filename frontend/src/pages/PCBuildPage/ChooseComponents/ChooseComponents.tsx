import ChooseComponent from './ChooseComponent/ChooseComponent.tsx';
import videoCardIcon from 'assets/videocard-white-icon.png';
import processorIcon from 'assets/cpu-white-icon.png';
import styles from './ChooseComponents.module.css';
import componentTypes from 'enums/componentTypes.ts';
import React from 'react';
import TProduct from 'types/TProduct.ts';
import useComponents from 'hooks/useComponents.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import { TUseBuildComponents } from 'hooks/useBuild.ts';
import TOneOfComponents from 'types/components/TOneOfComponents.ts';

function convertToTProduct(components: TOneOfComponents<string>[]): TProduct[] {
  return components.map(component => {
    return {
      productId: component.productId,
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
  setIsActive: (
    componentType: keyof typeof componentTypes,
    newIsActive: boolean,
  ) => void;
};

const ChooseComponents: React.FC<ChooseComponentsProps> = props => {
  const { components, getComponents } = useComponents();
  const dispatch = useDispatch();
  return (
    <section className={styles.blocks}>
      <ChooseComponent
        img={videoCardIcon}
        type={'videoCard'}
        title={'Видеокарта'}
        isImportant={true}
        errorType={props.components.videoCard.currentErrorType}
        searchTitle={'Выберите видеокарту'}
        products={
          components ? convertToTProduct(components.videoCard.components) : null
        }
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getComponents('videoCard').then(() => dispatch(setIsLoading(false)));
        }}
        currentProduct={props.components.videoCard.currentProduct}
        setCurrentProduct={props.setComponent}
        isActive={props.components.videoCard.isActive}
        setIsActive={props.setIsActive}
      />
      <ChooseComponent
        img={processorIcon}
        type={'processor'}
        title={'Процессор'}
        isImportant={true}
        errorType={props.components.processor.currentErrorType}
        searchTitle={'Выберите процессор'}
        products={
          components ? convertToTProduct(components.processor.components) : null
        }
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getComponents('processor').then(() => dispatch(setIsLoading(false)));
        }}
        currentProduct={props.components.processor.currentProduct}
        setCurrentProduct={props.setComponent}
        isActive={props.components.processor.isActive}
        setIsActive={props.setIsActive}
      />
      <ChooseComponent
        img={processorIcon}
        type={'motherBoard'}
        title={'Материнская плата'}
        isImportant={true}
        errorType={props.components.motherBoard.currentErrorType}
        searchTitle={'Выберите материнскую плату'}
        products={
          components
            ? convertToTProduct(components.motherBoard.components)
            : null
        }
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getComponents('motherBoard').then(() =>
            dispatch(setIsLoading(false)),
          );
        }}
        currentProduct={props.components.processor.currentProduct}
        setCurrentProduct={props.setComponent}
        isActive={props.components.motherBoard.isActive}
        setIsActive={props.setIsActive}
      />
    </section>
  );
};

export default ChooseComponents;
