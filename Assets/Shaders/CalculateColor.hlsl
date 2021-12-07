#ifndef CALCULATE_COLOR
#define CALCULATE_COLOR

const static int MaxColorsCount = 16;
int ColorsCount;
float4 LayersColors[MaxColorsCount];
float LayersHeights[MaxColorsCount];

void CalculateColor_float(float yPos, float maxHeight, out float r, out float g, out float b)
{
	for(int i = 0; i < ColorsCount; i++)
	{
		if (yPos > (maxHeight * LayersHeights[i]))
		{
			r = LayersColors[i].x;
			g = LayersColors[i].y;
			b = LayersColors[i].z;
		}
	}
}
#endif