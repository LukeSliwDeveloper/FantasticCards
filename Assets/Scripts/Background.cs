using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private SpritesContainer[] _backgroundsSprites;
    [SerializeField] private SpriteRenderer[] _backgroundRenderers;

    private Vector3 _basePosition = Vector3.up * .4f;
    private float _wrapPositionX = -25f;
    private float _baseScrollSpeed = .3f;
    private float _scrollSpeedPerLayer = -.04f;

    private Transform _scrolledTransform;
    private bool _followCamera;

    private void Update()
    {
        if (_followCamera)
        {

        }
        else
        {
            for (int i = 0; i < _backgroundRenderers.Length / 2; i++)
            {
                _scrolledTransform = _backgroundRenderers[i * 2].transform;
                _scrolledTransform.position += Vector3.left * Time.deltaTime * (_baseScrollSpeed + _scrollSpeedPerLayer * i);
                if (_scrolledTransform.position.x < _wrapPositionX)
                    _scrolledTransform.position += Vector3.left * _wrapPositionX;
            }
        }
    }

    public void ScrollRandomly()
    {
        _followCamera = false;
        transform.position = _basePosition;
        var randomBackgroundIndex = Random.Range(0, _backgroundsSprites.Length);
        for (int i = 0; i < _backgroundRenderers.Length / 2; i++)
        {
            _backgroundRenderers[i * 2].sprite = _backgroundsSprites[randomBackgroundIndex].Sprites[i];
            _backgroundRenderers[i * 2 + 1].sprite = _backgroundsSprites[randomBackgroundIndex].Sprites[i];
            _backgroundRenderers[i * 2].transform.position = Vector3.right * Random.Range(0f, _wrapPositionX);
        }
    }
}
