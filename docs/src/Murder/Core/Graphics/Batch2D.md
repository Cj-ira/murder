# Batch2D

**Namespace:** Murder.Core.Graphics \
**Assembly:** Murder.dll

```csharp
public class Batch2D : IDisposable
```

**Implements:** _[IDisposable](https://learn.microsoft.com/en-us/dotnet/api/System.IDisposable?view=net-7.0)_

### ⭐ Constructors
```csharp
public Batch2D(GraphicsDevice graphicsDevice, bool autoHandleAlphaBlendedSprites)
```

**Parameters** \
`graphicsDevice` [GraphicsDevice](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html) \
`autoHandleAlphaBlendedSprites` [bool](https://learn.microsoft.com/en-us/dotnet/api/System.Boolean?view=net-7.0) \

### ⭐ Properties
#### AllowIBasicShaderEffectParameterClone
```csharp
public bool AllowIBasicShaderEffectParameterClone { get; public set; }
```

**Returns** \
[bool](https://learn.microsoft.com/en-us/dotnet/api/System.Boolean?view=net-7.0) \
#### AutoHandleAlphaBlendedSprites
```csharp
public bool AutoHandleAlphaBlendedSprites { get; private set; }
```

Auto handle any non-opaque (i.e. with some transparency; Opacity &lt; 1.0f) sprite rendering.
            By drawing first all opaque sprites, with depth write enabled, followed by non-opaque sprites, with only depth read enabled.

**Returns** \
[bool](https://learn.microsoft.com/en-us/dotnet/api/System.Boolean?view=net-7.0) \
#### BatchMode
```csharp
public BatchMode BatchMode { get; private set; }
```

**Returns** \
[BatchMode](/Murder/Core/Graphics/BatchMode.html) \
#### BlendState
```csharp
public BlendState BlendState { get; private set; }
```

**Returns** \
[BlendState](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.BlendState.html) \
#### DepthStencilState
```csharp
public DepthStencilState DepthStencilState { get; private set; }
```

**Returns** \
[DepthStencilState](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.DepthStencilState.html) \
#### Effect
```csharp
public Effect Effect { get; public set; }
```

**Returns** \
[Effect](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.Effect.html) \
#### GraphicsDevice
```csharp
public GraphicsDevice GraphicsDevice { get; public set; }
```

**Returns** \
[GraphicsDevice](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.GraphicsDevice.html) \
#### IsBatching
```csharp
public bool IsBatching { get; private set; }
```

**Returns** \
[bool](https://learn.microsoft.com/en-us/dotnet/api/System.Boolean?view=net-7.0) \
#### IsDisposed
```csharp
public bool IsDisposed { get; private set; }
```

**Returns** \
[bool](https://learn.microsoft.com/en-us/dotnet/api/System.Boolean?view=net-7.0) \
#### RasterizerState
```csharp
public RasterizerState RasterizerState { get; private set; }
```

**Returns** \
[RasterizerState](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.RasterizerState.html) \
#### SamplerState
```csharp
public SamplerState SamplerState { get; private set; }
```

**Returns** \
[SamplerState](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SamplerState.html) \
#### StartBatchItemsCount
```csharp
public static const int StartBatchItemsCount;
```

**Returns** \
[int](https://learn.microsoft.com/en-us/dotnet/api/System.Int32?view=net-7.0) \
#### Transform
```csharp
public Matrix Transform { get; private set; }
```

**Returns** \
[Matrix](https://docs.monogame.net/api/Microsoft.Xna.Framework.Matrix.html) \
### ⭐ Methods
#### Dispose()
```csharp
public virtual void Dispose()
```

Immediately releases the unmanaged resources used by this object.

#### Begin(Effect, BatchMode, BlendState, SamplerState, DepthStencilState, RasterizerState, T?)
```csharp
public void Begin(Effect effect, BatchMode batchMode, BlendState blendState, SamplerState sampler, DepthStencilState depthStencil, RasterizerState rasterizer, T? transform)
```

**Parameters** \
`effect` [Effect](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.Effect.html) \
`batchMode` [BatchMode](/Murder/Core/Graphics/BatchMode.html) \
`blendState` [BlendState](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.BlendState.html) \
`sampler` [SamplerState](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SamplerState.html) \
`depthStencil` [DepthStencilState](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.DepthStencilState.html) \
`rasterizer` [RasterizerState](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.RasterizerState.html) \
`transform` [T?](https://learn.microsoft.com/en-us/dotnet/api/System.Nullable-1?view=net-7.0) \

#### Draw(Texture2D, Rectangle, Color, float)
```csharp
public void Draw(Texture2D texture, Rectangle destination, Color color, float sorting)
```

**Parameters** \
`texture` [Texture2D](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.Texture2D.html) \
`destination` [Rectangle](https://docs.monogame.net/api/Microsoft.Xna.Framework.Rectangle.html) \
`color` [Color](/Murder/Core/Graphics/Color.html) \
`sorting` [float](https://learn.microsoft.com/en-us/dotnet/api/System.Single?view=net-7.0) \

#### Draw(Texture2D, Rectangle, Color)
```csharp
public void Draw(Texture2D texture, Rectangle destination, Color color)
```

**Parameters** \
`texture` [Texture2D](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.Texture2D.html) \
`destination` [Rectangle](https://docs.monogame.net/api/Microsoft.Xna.Framework.Rectangle.html) \
`color` [Color](/Murder/Core/Graphics/Color.html) \

#### Draw(Texture2D, Vector2, Vector2, T?, float, Vector2, ImageFlip, Color, Vector2, Vector3, float)
```csharp
public void Draw(Texture2D texture, Vector2 position, Vector2 targetSize, T? sourceRectangle, float rotation, Vector2 scale, ImageFlip flip, Color color, Vector2 origin, Vector3 blendColor, float layerDepth)
```

**Parameters** \
`texture` [Texture2D](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.Texture2D.html) \
`position` [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html) \
`targetSize` [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html) \
`sourceRectangle` [T?](https://learn.microsoft.com/en-us/dotnet/api/System.Nullable-1?view=net-7.0) \
`rotation` [float](https://learn.microsoft.com/en-us/dotnet/api/System.Single?view=net-7.0) \
`scale` [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html) \
`flip` [ImageFlip](/Murder/Core/Graphics/ImageFlip.html) \
`color` [Color](/Murder/Core/Graphics/Color.html) \
`origin` [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html) \
`blendColor` [Vector3](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector3.html) \
`layerDepth` [float](https://learn.microsoft.com/en-us/dotnet/api/System.Single?view=net-7.0) \

#### Draw(Texture2D, Vector2, T?, float, Vector2, ImageFlip, Color, Vector2, Vector3, float)
```csharp
public void Draw(Texture2D texture, Vector2 position, T? sourceRectangle, float rotation, Vector2 scale, ImageFlip flip, Color color, Vector2 origin, Vector3 blendColor, float layerDepth)
```

**Parameters** \
`texture` [Texture2D](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.Texture2D.html) \
`position` [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html) \
`sourceRectangle` [T?](https://learn.microsoft.com/en-us/dotnet/api/System.Nullable-1?view=net-7.0) \
`rotation` [float](https://learn.microsoft.com/en-us/dotnet/api/System.Single?view=net-7.0) \
`scale` [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html) \
`flip` [ImageFlip](/Murder/Core/Graphics/ImageFlip.html) \
`color` [Color](/Murder/Core/Graphics/Color.html) \
`origin` [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html) \
`blendColor` [Vector3](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector3.html) \
`layerDepth` [float](https://learn.microsoft.com/en-us/dotnet/api/System.Single?view=net-7.0) \

#### End()
```csharp
public void End()
```

#### Flush(bool)
```csharp
public void Flush(bool includeAlphaBlendedSprites)
```

Send all stored batches to rendering, but doesn't end batching.
            If auto handle alpha blended sprites is active, be careful! Since it can includes alpha blended sprites too.

**Parameters** \
`includeAlphaBlendedSprites` [bool](https://learn.microsoft.com/en-us/dotnet/api/System.Boolean?view=net-7.0) \
\



⚡