import { useEffect, useState } from 'react';
import TProduct from 'types/TProduct.ts';
import EComponentTypes from 'enums/EComponentTypes.ts';
import EBuildBlockErrors from 'enums/EBuildBlockErrors.ts';
import useBorderValues from 'hooks/useBorderValues.ts';

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
  const [progressOfBuild, setProgressOfBuild] = useBorderValues(0, 0, 100);
  const [price, setPrice] = useBorderValues(0, 0);
  const [power, setPower] = useBorderValues(0, 0, 10);

  useEffect(() => {
    calculateProgressOfBuild();
    calculatePrice();
    calculatePower();
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

  function calculatePower() {
    let key: keyof typeof components;
    let sumPower = 0;
    let countComponents = 0;
    for (key in components) {
      countComponents += 1;
      sumPower += components[key].currentProduct
        ? components[key].currentProduct!.power
        : 0;
    }
    setPower(sumPower / countComponents);
  }

  return {
    setComponent,
    toggleError,
    components,
    progressOfBuild,
    price,
    power,
  };
}

export default useBuild;
