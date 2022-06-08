import { PropsWithChildren, useState } from "react";
import styled from "styled-components";


export interface RotationWrapperProps {
  rotation: string;
}

export function RotationWrapper(props: PropsWithChildren<RotationWrapperProps>) {
  const [rotatedElement, setRotatedElement] = useState<HTMLElement|null>(null);
  const boundingClientRect = rotatedElement?.getBoundingClientRect();
  const width = boundingClientRect
    ? Math.abs(boundingClientRect.right - boundingClientRect.left)
    : undefined;
  const height = boundingClientRect
    ? Math.abs(boundingClientRect.top - boundingClientRect.bottom)
    : undefined; 

  return <RotationContainer width={width} height={height}>
    <RotatedElement ref={setRotatedElement} rotation={props.rotation}>
        {props.children}
    </RotatedElement>
  </RotationContainer>
}

const RotationContainer = styled.div<{ height?: number, width?: number }>`
  box-sizing: content-box;
  height: ${p => p.height}px;
  width: ${p => p.width}px;
  display: flex;
  align-items: center;
  justify-content: center; 
`

const RotatedElement = styled.div<{ rotation: string }>`
  transform: rotate(${p => p.rotation});
`