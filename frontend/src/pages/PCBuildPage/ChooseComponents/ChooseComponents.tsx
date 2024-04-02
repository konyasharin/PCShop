import ChooseComponent from './ChooseComponent/ChooseComponent.tsx';
import videoCardIcon from 'assets/videocard-white-icon.png';
import processorIcon from 'assets/cpu-white-icon.png';
import RAMIcon from 'assets/ram-white-icon.png';
import motherBoardIcon from 'assets/motherboard-white-icon.png';
import coolerIcon from 'assets/cooling-white-icon.png';
import computerCaseIcon from 'assets/computercase-white-icon.png';
import powerUnitIcon from 'assets/powerunit-white-icon.png';
import diskIcon from 'assets/disk-white-icon.png';
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
        img={motherBoardIcon}
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
        currentProduct={props.components.motherBoard.currentProduct}
        setCurrentProduct={props.setComponent}
        isActive={props.components.motherBoard.isActive}
        setIsActive={props.setIsActive}
      />
      <ChooseComponent
        img={RAMIcon}
        type={'RAM'}
        title={'Оперативная память'}
        isImportant={true}
        errorType={props.components.RAM.currentErrorType}
        searchTitle={'Выберите оперативную память'}
        products={
          components ? convertToTProduct(components.RAM.components) : null
        }
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getComponents('RAM').then(() => dispatch(setIsLoading(false)));
        }}
        currentProduct={props.components.RAM.currentProduct}
        setCurrentProduct={props.setComponent}
        isActive={props.components.RAM.isActive}
        setIsActive={props.setIsActive}
      />
      <ChooseComponent
        img={computerCaseIcon}
        type={'computerCase'}
        title={'Корпус'}
        isImportant={true}
        errorType={props.components.computerCase.currentErrorType}
        searchTitle={'Выберите корпус'}
        products={
          components
            ? convertToTProduct(components.computerCase.components)
            : null
        }
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getComponents('computerCase').then(() =>
            dispatch(setIsLoading(false)),
          );
        }}
        currentProduct={props.components.computerCase.currentProduct}
        setCurrentProduct={props.setComponent}
        isActive={props.components.computerCase.isActive}
        setIsActive={props.setIsActive}
      />
      <ChooseComponent
        img={coolerIcon}
        type={'cooler'}
        title={'Кулер'}
        isImportant={true}
        errorType={props.components.cooler.currentErrorType}
        searchTitle={'Выберите кулер'}
        products={
          components ? convertToTProduct(components.cooler.components) : null
        }
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getComponents('cooler').then(() => dispatch(setIsLoading(false)));
        }}
        currentProduct={props.components.cooler.currentProduct}
        setCurrentProduct={props.setComponent}
        isActive={props.components.cooler.isActive}
        setIsActive={props.setIsActive}
      />
      <ChooseComponent
        img={powerUnitIcon}
        type={'powerUnit'}
        title={'Блок питания'}
        isImportant={true}
        errorType={props.components.powerUnit.currentErrorType}
        searchTitle={'Выберите блок питания'}
        products={
          components ? convertToTProduct(components.powerUnit.components) : null
        }
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getComponents('powerUnit').then(() => dispatch(setIsLoading(false)));
        }}
        currentProduct={props.components.powerUnit.currentProduct}
        setCurrentProduct={props.setComponent}
        isActive={props.components.powerUnit.isActive}
        setIsActive={props.setIsActive}
      />
      <ChooseComponent
        img={diskIcon}
        type={'SSD'}
        title={'SSD'}
        isImportant={true}
        errorType={props.components.SSD.currentErrorType}
        searchTitle={'Выберите SSD'}
        products={
          components ? convertToTProduct(components.SSD.components) : null
        }
        onShowMore={() => {
          dispatch(setIsLoading(true));
          getComponents('SSD').then(() => dispatch(setIsLoading(false)));
        }}
        currentProduct={props.components.SSD.currentProduct}
        setCurrentProduct={props.setComponent}
        isActive={props.components.SSD.isActive}
        setIsActive={props.setIsActive}
      />
    </section>
  );
};

export default ChooseComponents;
