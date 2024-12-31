# How to Set Up the Stencil Buffer

There are two methods to set up the stencil buffer:

1. Using the **StencilBufferMask** shader and URP's render features.
2. Using the **StencilBufferMask** shader directly with any shader capable of modifying the stencil buffer ID (e.g., the **StencilBufferLit**, a clone of the URP default Lit shader modified to use the stencil buffer).

## Prerequisites for Both Methods
Ensure the **Depth Priming Mode** is set to **Disabled** in the Rendering options of the Renderer Asset. The stencil buffer will not function correctly otherwise.

---

## Method 1: Render Feature Method
### Pros
- Can mask any other shader directly.
- Highly versatile.

### Cons
- Requires layers and a render feature for each stencil buffer ID.
  - Two render features are needed if masking both opaque and transparent materials.
- Involves additional setup.

### Process
1. **Scene Setup**
   - Add the mask object with a material using the **StencilBufferMask** shader.
     - Example: Set StencilID to **1**, Operation to **Always**, and Action to **Replace**.
   - Add the objects to be masked with any shader you prefer.

2. **Layer Configuration**
   - Create a new layer for the masked objects and assign it to the relevant objects.

3. **Renderer Asset Configuration**
   - Locate the Renderer Asset being used.
   - Update the filtering in the Renderer Asset settings:
     - Modify the **Opaque Layer Mask** and **Transparent Layer Mask** to exclude the new layer for the masked items.

4. **Add Render Features**
   - Create a new **Render Objects** render feature and configure it:
     - **Name** it appropriately.
     - Set the **Event** to **AfterRenderingOpaques**.
     - In the **Filters** section, set the **Layer Mask** to the newly created layer.
     - In the **Overrides** section:
       - Check the **Stencil** option.
       - Set the **Stencil Value** to match the StencilID of the mask (e.g., 1).
       - Set the **Compare Function** to **Equal** (leave Pass, Fail, and Z Fail as **Keep**).
   
5. **Mask Transparent Materials (Optional)**
   - Duplicate the render feature and:
     - Set the **Event** to **AfterRenderingTransparents**.
     - Update the **Queue** to **Transparent**.

6. **Repeat for Additional StencilIDs**
   - For each unique StencilID, create a corresponding render feature (or two if masking both opaque and transparent materials).

---

## Method 2: Paired Shaders Method
### Pros
- Does not require layers or render features.
- Easier to set up and use.

### Cons
- Only works with shaders designed for the stencil buffer.

### Process
1. **Scene Setup**
   - Add the mask object with a material using the **StencilBufferMask** shader.
     - Example: Set StencilID to **1**, Operation to **Always**, and Action to **Replace**.

2. **Add Masked Objects**
   - Use materials with shaders that utilize the stencil buffer (e.g., **StencilBufferLit**).

3. **Ensure StencilID Match**
   - The materials for both the mask and the masked objects must use the same StencilID.

---

