import { useEffect, useState } from 'react';
import TProduct from 'types/TProduct.ts';
import EComponentTypes from 'enums/EComponentTypes.ts';
import EBuildBlockErrors from 'enums/EBuildBlockErrors.ts';

export type TUseBuildComponents = {
  [componentType in EComponentTypes]: {
    currentProduct: TProduct | null;
    currentErrorType: EBuildBlockErrors;
  };
};

function useBuild() {
  const [components, setComponents] = useState<TUseBuildComponents>({
    videoCard: {
      currentProduct: null,
      currentErrorType: EBuildBlockErrors.Error,
    },
    processor: {
      currentProduct: null,
      currentErrorType: EBuildBlockErrors.Error,
    },
  });
  const [progressOfBuild, setProgressOfBuild] = useState(0);
  const [price, setPrice] = useState(0);

  useEffect(() => {
    calculateProgressOfBuild();
    calculatePrice();
  }, [components]);
  function toggleError(
    newError: EBuildBlockErrors,
    componentType: EComponentTypes,
  ) {
    setComponents({
      ...components,
      [componentType]: {
        ...components[componentType],
        currentErrorType: newError,
      },
    });
  }

  function setComponent(
    newProduct: TProduct | null,
    componentType: EComponentTypes,
  ) {
    setComponents({
      ...components,
      [componentType]: {
        ...components[componentType],
        currentProduct: newProduct,
        currentErrorType: newProduct
          ? EBuildBlockErrors.Success
          : EBuildBlockErrors.Error,
      },
    });
  }
  function calculateProgressOfBuild() {
    let key: keyof typeof components;
    let choseProducts = 0;
    let countProducts = 0;
    for (key in components) {
      countProducts += 1;
      if (components[key].currentProduct !== null) {
        choseProducts += 1;
      }
    }
    setProgressOfBuild((choseProducts / countProducts) * 100);
  }

  function calculatePrice() {
    let key: keyof typeof components;
    let newPrice = 0;
    for (key in components) {
      newPrice += components[key].currentProduct
        ? components[key].currentProduct!.price
        : 0;
    }
    setPrice(newPrice);
  }

  return { setComponent, toggleError, components, progressOfBuild, price };
}

export default useBuild;
