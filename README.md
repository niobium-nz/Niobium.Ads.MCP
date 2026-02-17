# AdsTransparency

## CI: Docker build/push

This repo includes a GitHub Actions workflow at `.github/workflows/docker.yml` that:

- runs on `pull_request` to `main`: builds the Docker image (no push)
- runs on `push` to `main`: builds and pushes the Docker image to Docker Hub

### Docker Hub image

- `docker.io/5he11/adslibrary:latest` (only on default branch)
- `docker.io/5he11/adslibrary:sha-<gitsha>`

### Required GitHub repository secrets

Create these repository secrets:

- `DOCKERHUB_USERNAME`: your Docker Hub username (e.g., `5he11`)
- `DOCKERHUB_TOKEN`: a Docker Hub access token/password with permission to push to `5he11/adslibrary`