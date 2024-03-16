import { useRef, useState } from 'react';
import getComponentsFromDatabase from 'api/components/getComponents.ts';

function useComponents<TResponse>(url: string) {
  const [components, setComponents] = useState<TResponse[]>([]);
  const offset = useRef(0);
  async function getComponents() {
    const response = await getComponentsFromDatabase<TResponse[]>(
      url,
      3,
      offset.current,
    );
    setComponents([...components, ...response.data.data]);
    offset.current = offset.current + 3;
  }

  return { components, getComponents };
}

export default useComponents;
